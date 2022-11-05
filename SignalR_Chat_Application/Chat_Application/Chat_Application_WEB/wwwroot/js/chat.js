

var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

connectionChat.on("ReceiveUserConnected", function (userId, userName) {
    addMessage(`${userName} is opened connection`)
});

connectionChat.on("ReceiveUserDisconnected", function (userId, userName) {
    addMessage(`${userName} is closed connection!`);
});

connectionChat.on("ReceiveAddRoomMessage", function (maxRoom, roomId, roomName, userId, userName) {
    addMessage(`${userName} has created room ${roomName}`);
    fillRoomDropDown();
});

connectionChat.on("ReceiveDeleteRoomMessage", function (roomName, userId, userName) {
    addMessage(`${userName} has deleted room ${roomName}`);
    fillRoomDropDown();
});

connectionChat.on("ReceivePublicMessage", function (roomId, userId, userName, message, roomName) {
    addMessage(`[Public Message - ${roomName}]${userName} says ${message}`);
});

connectionChat.on("ReceivePrivvateMessage", function (senderId, senderName, message, chatId, receiverName) {
    addMessage(`[Private Message from ${senderName} to ${receiverName}] ${message}`);
});

function sendPrivateMessage() {
    let inputMsg = document.getElementById('txtPrivateMessage');
    let ddlSelUser = document.getElementById('ddlSelUser');

    let receiverId = ddlSelUser.value;
    let receiverName = ddlSelUser.options[ddlSelUser.selectedIndex].text;
    var message = inputMsg.value;

    connectionChat.send("SendPrivateMessage", receiverId, message, receiverName);
    inputMsg.value = '';
}

function sendPublicMessage() {
    let inputMsg = document.getElementById('txtPublicMessage');
    let ddlSelRoom = document.getElementById('ddlSelRoom');

    let roomId = ddlSelRoom.value;
    let roomName = ddlSelRoom.options[ddlSelRoom.selectedIndex].text;
    var message = inputMsg.value;

    connectionChat.send("SendPublicMessage", Number(roomId), message, roomName);
    inputMsg.value = '';
}

function addnewRoom(maxRoom) {
    let createRoomName = document.getElementById('createRoomName');

    var roomName = createRoomName.value;

    if (roomName == null && roomName == '') {
        return;
    }

    /*POST*/
    $.ajax({
        url: '/ChatRooms/PostChatRoom',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: 0, name: roomName }),
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*ADD ROOM COMPLETED SUCCESSFULLY*/
            connectionChat.send("SendAddRoomMessage", maxRoom, json.id, json.name);
            createRoomName.value = '';
        },
        error: function (xhr) {
            alert('error');
        }
    })
}

function deleteRoom() {
    let deleteRoomName = document.getElementById('ddlDelRoom');

    var roomId = deleteRoomName.value;
    var roomName = deleteRoomName.options[deleteRoomName.selectedIndex].text;

    let text = `Do You want to delete chat room ${roomName}?`;

    if (confirm(text) == false) {
        return;
    }

    if (roomId == null && roomId == '') {
        return;
    }

    /*DELETE*/
    $.ajax({
        url: `/ChatRooms/DeleteChatRoom/${roomId}`,
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*DELETE ROOM COMPLETED SUCCESSFULLY*/
            connectionChat.send("SendDeleteRoomMessage", json.name);
        },
        error: function (xhr) {
            alert('error');
        }
    })
}

document.addEventListener('DOMContentLoaded', (event) => {
    fillRoomDropDown();
    fillUserDropDown();
})

function fillUserDropDown() {
    $.getJSON('/ChatRooms/GetChatUser')
        .done(function (json) {
            var ddlSelUser = document.getElementById("ddlSelUser");

            ddlSelUser.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");

                newOption.text = item.userName;//item.whateverProperty
                newOption.value = item.id;
                ddlSelUser.add(newOption);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });
}

function fillRoomDropDown() {
    $.getJSON('/ChatRooms/GetChatRoom')
        .done(function (json) {
            var ddlDelRoom = document.getElementById("ddlDelRoom");
            var ddlSelRoom = document.getElementById("ddlSelRoom");

            ddlDelRoom.innerText = null;
            ddlSelRoom.innerText = null;

            json.forEach(function (item) {
                var newOption = document.createElement("option");

                newOption.text = item.name;
                newOption.value = item.id;
                ddlDelRoom.add(newOption);

                var newOption1 = document.createElement("option");

                newOption1.text = item.name;
                newOption1.value = item.id;
                ddlSelRoom.add(newOption1);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });
}

function addMessage(msg) {
    if (msg == null && msg == '') {
        return;
    }
    let ui = document.getElementById("messagesList");
    let li = document.createElement("li");
    li.innerHTML = msg;
    ui.appendChild(li);
}

connectionChat.start();
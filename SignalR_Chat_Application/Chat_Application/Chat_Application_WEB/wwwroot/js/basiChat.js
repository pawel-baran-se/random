var connectionChat = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

document.getElementById("sendMessage").disabled = true;

connectionChat.on("MessageReceived", function (user, message, hour) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${hour} ${user}: ${message}`;
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var sender = document.getElementById("senderEmail").value;
    var message = document.getElementById("chatMessage").value;
    var receiver = document.getElementById("receiverEmail").value;

    //
    if (receiver.length > 0) {
        connectionChat.send("SendMessageToReceiver", sender, receiver, message);
    } else {
        connectionChat.send("SendMessageToAll", sender, message);
    }

    event.preventDefault();
})

connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
    console.log("Connection with the chat!");
});
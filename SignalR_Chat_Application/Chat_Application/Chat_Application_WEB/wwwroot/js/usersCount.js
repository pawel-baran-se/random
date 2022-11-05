

//create connection
var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

//connect to methods that hub invokes
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
})

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUserCounter");
    newCountSpan.innerText = value.toString();
})

//invoke hub methods
function newWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
}


//start connection
function fulfilled() {
    //on start
    console.log("Successful connection")
    newWindowLoadedOnClient();
}

function rejected() {
    //rejected logs
}

connectionUserCount.start().then(fulfilled, rejected);
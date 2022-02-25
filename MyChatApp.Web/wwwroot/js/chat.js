let myModal = new bootstrap.Modal(document.getElementById('setUserNameModal'), {
    keyboard: false
});

const channelName = document.getElementById("channelNameArea");
const joinChannelButtons = document.getElementsByClassName("join-channel-button");
const messageBox = document.getElementById('message');
const messages = document.getElementById('chatArea');
const messageSendArea = document.getElementById('messageSendArea');
let clientUserName = localStorage.getItem("userName");
let activeChannelName = null;

const joinChannel = function () {
    let attribute = this.getAttribute("data-channelName");
    channelName.textContent = attribute;
    activeChannelName = attribute;
    messageSendArea.classList.remove("d-none");
    messageSendArea.classList.add("d-flex");

    $.ajax({
        url: "/home/getTalkHistory",
        data: { channelName: activeChannelName },
        method: "post",
        success: function (response) {
            messages.innerHTML = '';
            response.forEach((item, index) => {
                createMessageNode(item.userName, item.content);
            });
        },
        error: function (response) {
            console.error("error", response);
        }
    });

    connection.invoke('JoinGroup', activeChannelName);
};

for (let joinButton of joinChannelButtons) {
    joinButton.addEventListener('click', joinChannel, false);
}

if (clientUserName === null) {
    myModal.show();
}

const setUserName = () => {
    clientUserName = document.getElementById("userNameInput").value;
    localStorage.setItem('userName', clientUserName);
    myModal.hide();
}

const sendClientMessage = () => {
    connection.invoke('SendMessageEveryone', messageBox.value, clientUserName);
    messageBox.value = '';
}

const sendGroupMessage = () => {
    connection.invoke('SendGroupMessage', activeChannelName, messageBox.value, clientUserName);
    messageBox.value = '';
};

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatLobby")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start()
    .then(() => console.log('connected!'))
    .catch(console.error);

messageBox.addEventListener('keypress', function (e) {
    if (e.key === 'Enter')
        sendGroupMessage();
});
document.querySelector('#sendMessageButton').addEventListener('click', sendGroupMessage());

connection.on('GroupMessageListener', (sender, messageText) => {
    createMessageNode(sender, messageText);
});

const createMessageNode = (userName, message) => {
    var child = document.createElement("div");
    child.setAttribute("class", "sender-name");
    const childContent = document.createTextNode(userName);
    child.appendChild(childContent);

    var parent = document.createElement("div");
    parent.setAttribute("class", "chat-item");
    const parentContent = document.createTextNode(message);

    parent.appendChild(child);
    parent.appendChild(parentContent);

    messages.appendChild(parent);
}
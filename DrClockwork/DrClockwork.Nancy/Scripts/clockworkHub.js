$(function() {
    var hub = $.connection.clockworkHub;

    hub.client.broadcastAnswer = function (question, name) {
        debugger;
        $('#questions').append($('<span />', {
            text: question + ' - ' + name
        }));
    };

    $.connection.hub.start();
})
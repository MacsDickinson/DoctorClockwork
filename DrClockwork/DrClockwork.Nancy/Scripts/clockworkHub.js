$(function() {
    var hub = $.connection.clockworkHub;

    hub.client.broadcastAnswer = function (question, answer) {

        $('#questions').append('<div class="question"><span>' + question + ' - ' + answer + '</span></div>');
    };

    $.connection.hub.start();
})
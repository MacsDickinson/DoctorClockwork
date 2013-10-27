$(function() {
    var hub = $.connection.clockworkHub;

    hub.client.broadcastAnswer = function (question, answer, name) {
        var $question = $('<div \>', {
            'class': 'bubble bubble-question bubble-new fade'
        });
        $question.append($('<h3 />', {
            text: name
        }));
        $question.append($('<p />', {
            text: question
        }));
        $('#questions').prepend($question);
        var $answer = $('<div \>', {
            'class': 'bubble bubble-answer bubble-new fade'
        });
        $answer.append($('<h3 />', {
            text: 'Dr Clockwork'
        }));
        $answer.append($('<p />', {
            text: answer
        }));
        $('#questions').prepend($answer);
    };

    $.connection.hub.start();
})
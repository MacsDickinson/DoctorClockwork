$(function() {
    var hub = $.connection.clockworkHub;

    hub.client.broadcastAnswer = function (question, answer, name) {
        var $convo = $('<div />', {            
            'class': 'conversation'
        });
        var $answer = $('<div \>', {
            'class': 'bubble bubble-answer bubble-new fade'
        });
        $answer.append($('<h3 />', {
            text: 'Dr Clockwork'
        }).append('<span />', {
            'class': 'glyphicon glyphicon-phone'
        }));
        $answer.append($('<p />', {
            text: ' ' + answer
        }));
        $convo.append($answer);
        var $question = $('<div \>', {
            'class': 'bubble bubble-question bubble-new fade'
        });
        $question.append($('<h3 />', {
            text: name
        }).append('<span />', {
            'class': 'glyphicon glyphicon-phone'
        }));
        $question.append($('<p />', {
            text: question
        }));
        $convo.append($question);
        $('#questions').prepend($convo);
    };

    $.connection.hub.start();
})
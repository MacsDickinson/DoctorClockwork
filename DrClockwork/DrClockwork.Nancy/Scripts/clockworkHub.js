$(function() {
    var hub = $.connection.clockworkHub;

    hub.client.broadcastAnswer = function (question, answer, name) {
        var $convo = $('<div />', {            
            'class': 'conversation'
        });
        var $answer = $('<div \>', {
            'class': 'bubble bubble-answer bubble-new fade'
        });
        var $aH = $('<h3 />', {
            text: ' Dr Clockwork'
        });
        $aH.prepend('<span />', {
            'class': 'glyphicon glyphicon-phone'
        });
        $answer.append($aH);
        $answer.append($('<p />', {
            text: ' ' + answer
        }));
        $convo.append($answer);
        var $question = $('<div \>', {
            'class': 'bubble bubble-question bubble-new fade'
        });
        var $qh = $('<h3 />', {
            text: ' ' + name
        });
        $qh.prepend('<span />', {
            'class': 'glyphicon glyphicon-phone'
        });
        $question.append($qh);
        $question.append($('<p />', {
            text: question
        }));
        $convo.append($question);
        $('#questions').prepend($convo);
        var count = parseInt($('#question-count').val()) + 1;
        $('#question-count').val(count);
    };

    $.connection.hub.start();
})
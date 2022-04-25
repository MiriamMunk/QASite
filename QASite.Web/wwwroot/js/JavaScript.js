$(() => {
    setInterval(() => {
        const id = $("#question-id").val();
        $.get('/likes/GetQuestionlikes', { id }, function ({ likes }) {
            $("#likes-count").text(likes);
        });
    }, 1000);

    $("#like-btn").on('click', function () {
        const QuestionID = $("#question-id").val();
        $.post('/likes/AddQuestionLike', { QuestionID }, function () {
            $("#like-btn").prop('disabled', true);
        });
    });

    $("#answers").on('click', '#answer-like-btn', function () {
        const btn = $(this);
        const AnswerId = $(btn).data('answr-id');
        $.post('/likes/AddAnswerLike', { AnswerId }, function () {
            $(btn).prop('disabled', true);
        });
    });
})
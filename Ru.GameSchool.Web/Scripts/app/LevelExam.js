$(document).ready(function () {
    var item = $('#LevelExamQuestionId');
    $('.addexamquestionanswer').click(function () {
        var questionId = $(this).data('question');
        //alert(questionId);
        item.val(questionId);
    });
});
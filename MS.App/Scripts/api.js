(function () {
    //attach events
    $('[data-taskid]').click(function () {
        var that = $(this);
        getComments(that);
    });

    $('[data-openadd]').click(function (event) {
        var taskId = $(event.target).attr('data-openadd');
        $('#addcomment-' + taskId)[0].reset();
        $('[data-savecomment=' + taskId + ']').prop('disabled', false);
        $(event.target).hide();
        $('[data-addcomment=' + taskId + ']').show();
        $('[data-savecomment=' + taskId + ']').off().click(addComment);

        function addComment() {
            $('[data-savecomment=' + taskId + ']').prop('disabled', true);
            event.preventDefault();
            $('#addcomment-' + taskId).validate();
            if ($('#addcomment-' + taskId).valid()) {
                var content = $('#content-' + taskId).val();
                var type = $('#type-' + taskId).val();
                var date = $('#remainderdate-' + taskId).val();

                var comment = { Content: content, Type: type, RemainderDate: date, TaskId: Number(taskId) };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(comment),
                    url: "api/addcomment",
                    contentType: "application/json"
                }).done(function () {
                    showInfo("Comment added");
                    var task = $('[data-taskid="' + taskId + '"]');
                    getComments(task);
                    $(event.target).show();
                    $('[data-addcomment=' + taskId + ']').hide();
                });
            }
        }
    });

    $(document).ajaxError(function (a, error) {
        handleAjaxError(error);
    });

    function handleAjaxError(response) {
        var errorMsg = JSON.stringify(response);
        if (response.readyState === 0) {
            errorMsg = "Cannot connect due to network error.";
        }
        if (response.responseText) {
            var errObj = JSON.parse(response.responseText);
            if (errObj.Message) {
                errorMsg = errObj.Message;
            }
        }
        showError(errorMsg);
    }

    function showInfo(message) {
        $('#infoBox').text(message);
        $('#infoBox').show();
        setTimeout(function () {
            $('#infoBox').fadeOut();
        }, 5000);
    }

    function showError(errorMsg) {
        $('#errorBox').text("Error: " + errorMsg);
        $('#errorBox').show();
    }

    function getComments(that) {
        var id = that.attr('data-taskid');
        commentsRequest(id)
            .done(function (data) {
                displayComments(data, that);
            });

        function commentsRequest(taskId) {
            var url = '/api/comments/' + taskId;
            return $.get(url);
        }
    }

    function buildComment(commentJson, number) {
        return `<div class="single-comment row" data-commentid="${commentJson.Id}">
                  <div class="col-sm-3">
                     <p>${commentJson.UserName}</p>
                     <p>${commentJson.CommentType}</p>
                     <p>${commentJson.DateAdded}</p>
                     <div class ="row" data-editremove>
                        <div class ="col-sm-2"><a href="javascript:void(0)" data-editcomment="${commentJson.Id}">Edit</a></div>
                        <div class ="col-sm-2"><a href="javascript:void(0)" onclick="return confirm('Are you sure?')" data-removecomment="${commentJson.Id}">Remove</a></div>
                     </div>
                     <div class ="row" data-savecomment>
                        <div class ="col-sm-2"><a href="javascript:void(0)" data-savecomment="${commentJson.Id}">Save</a></div>
                     </div>
                  </div>
                  <div class ="col-sm-9">
                     <p>#${number + 1}</p>
                     <p data-comment>${commentJson.Content}</p>
                  </div>
                </div>`;
    }

    function displayComments(data, taskElement) {
        $('[data-commentpanel]').hide();
        var id = taskElement.attr('data-taskid');
        var commentsPanel = $('[data-commentpanel="' + id + '"]');
        var commentsWrapper = commentsPanel.find('[data-comments]');
        commentsWrapper.empty();
        commentsPanel.show();

        if (data.Comments.length === 0) {
            commentsWrapper.append('<div>No Comments</div>');
        }

        for (var i = 0; i < data.Comments.length; i++) {
            var comment = buildComment(data.Comments[i], i);
            commentsWrapper.append(comment);
        }

        commentsWrapper.find('[data-savecomment]').hide();
        addCommentEventsListner();
        editCommentEventsListner();

        function addCommentEventsListner() {
            $('[data-removecomment]').off().click(function () {
                var commentId = $(this).attr('data-removecomment');
                var url = '/api/remove/' + commentId;

                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: success
                });

                function success() {
                    $('[data-commentid="' + commentId + '"]').remove();
                }
            });
        }

        function editCommentEventsListner() {
            $('[data-editcomment]').off().click(function() {
                var commentWrapper = $(this).closest('[data-commentid]');
                var contentElement = commentWrapper.find('[data-comment]');
                commentWrapper.find('[data-editremove]').hide();
                commentWrapper.find('[data-savecomment]').show();
                var oldContent = commentWrapper.find('[data-comment]').text();
                var input = $('<input type="text" data-newcontent class="form-control" />').val(oldContent);
                contentElement.replaceWith(input);

                $('[data-savecomment]').off().click(function () {
                    var newContent = commentWrapper.find('[data-newcontent]').val();
                    var commentId = commentWrapper.attr('data-commentid');
                    var commentObj = { Content: newContent, Id: commentId };
                    $.ajax({
                        type: "POST",
                        data: JSON.stringify(commentObj),
                        url: "api/edit",
                        contentType: "application/json"
                    }).done(function () {
                        showInfo("Changes saved");
                        getComments(taskElement);
                        commentWrapper.find('[data-editremove]').show();
                        commentWrapper.find('[data-savecomment]').hide();
                    });
                });
            });
        }
    }
})();
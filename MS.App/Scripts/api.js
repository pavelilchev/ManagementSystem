(function () {
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
        }, 3000);
    }

    function showError(errorMsg) {
        $('#errorBox').text("Error: " + errorMsg);
        $('#errorBox').show();
    }

    $(document).ajaxError(function(a, b,c) {
        handleAjaxError(b);
    });

    var commentsRequest = function (taskId) {
        var url = '/api/comments/' + taskId;
        return $.get(url);
    }

    function buildComment(commentJson, number) {
        return `<div class="single-comment row" data-commentid="${commentJson.Id}">
                  <div class="col-sm-3">
                     <p>${commentJson.UserName}</p>
                     <p>${commentJson.DateAdded}</p>
                     <div class ="row">
                        <div class ="col-sm-2"><a href="">Edit</a></div>
                        <div class ="col-sm-2"><a href="javascript:void(0)" onclick="return confirm('Are you sure?')" data-removecomment="${commentJson.Id}">Remove</a></div>
                     </div>
                  </div>
                  <div class ="col-sm-9">
                     <p>#${number + 1}</p>
                     <p>${commentJson.Content}</p>
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

        addCommentEventsListner();
    }

    function addCommentEventsListner() {
        $('[data-removecomment]').click(function () {
            var id = $(this).attr('data-removecomment');
            var url = '/api/remove/' + id;

            $.ajax({
                url: url,
                type: 'DELETE',
                success: success
            });

            function success() {
                $('[data-commentid="' + id + '"]').remove();
            }
        });
    }

    $('[data-taskid]').click(function () {
        var that = $(this);
        var id = that.attr('data-taskid');
        commentsRequest(id)
            .done(function (data) {
                displayComments(data, that);
            });
    });
})();
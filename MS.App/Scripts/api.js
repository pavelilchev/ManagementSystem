(function () {
    var baseUrl = '/api/comments/';

    var commentsRequest = function (taskId) {
        var url = baseUrl + taskId;
        return $.get(url);
    }

    function buildComment(commentJson) {
        return `<div class="single-comment row">
              <div class="col-sm-3">
                 <p>${commentJson.UserName}</p>
                 <p>${commentJson.DateAdded}</p>
              </div>
              <div class ="col-sm-9">
                 <p>#${commentJson.Id}</p>
                 <p>${commentJson.Content}</p>
             </div>
            </div>`;
    }

    function displayComments(data, taskElement) {
        $('[data-comments').hide();
        var id = taskElement.attr("data-taskid");
        var commentsWrapper = $('[data-comments="' + id + '"]');
        commentsWrapper.empty();
        commentsWrapper.show();

        if (data.Comments.length === 0) {
            commentsWrapper.append('<div>No Comments</div>');
        }

        for (var i = 0; i < data.Comments.length; i++) {
            var comment = buildComment(data.Comments[i], commentsWrapper);
            commentsWrapper.append(comment);
        }
    }

    $('[data-taskid]').click(function() {
        var that = $(this);
        var id = that.attr("data-taskid");
        commentsRequest(id)
            .done(function(data) {
                displayComments(data, that);
            });
    });
})();
$(() => {
    $('#get-joke').on('click', function () {
        $.get('/home/GetJoke', result => {
            $("#setup").text(result.setup);
            $("#punchline").text(result.punchLine);
            $("#joke-id").val(result.id);
            getLikesAndDislikes(result.id);
        });
    });

    $('#like').on('click', function () {
        const joke = {
            websiteId: $("#joke-id").val(),
            setup: $("#setup").text(),
            punchLine: $("#punchline").text()
        };
        $.post('/home/LikeJoke', { joke }, function () {
            getLikesAndDislikes(joke.websiteId);
        });
    });

    $('#unlike').on('click', function () {
        const joke = {
            websiteId: $("#joke-id").val(),
            setup: $("#setup").text(),
            punchLine: $("#punchline").text()
        };
        $.post('/home/DislikeJoke', { joke }, function () {
            getLikesAndDislikes(joke.websiteId);
        });
    });

    const getLikesAndDislikes = (id) => {
        $.get(`/home/GetLikesDislikes?websiteId=${id}`, result => {
            var today = new Date();
            $("#like").html(`${result.likes} <span class="glyphicon glyphicon-thumbs-up">`);
            $("#unlike").html(`${result.dislikes} <span class="glyphicon glyphicon-thumbs-down">`);
            var cDate = new Date(Date.parse(result.date));
            if (result.didUserLikeOrDislike) {
                if (today - cDate > 1 * 60 * 1000) {
                    $("#like").prop('disabled', true);
                    $("#unlike").prop('disabled', true);
                } else {
                    if (result.userLiked) {
                        $("#like").prop('disabled', true);
                        $("#unlike").prop('disabled', false);
                    } else {
                        $("#like").prop('disabled', false);
                        $("#unlike").prop('disabled', true); 
                    }
                }
            } else {
                $("#like").prop('disabled', false);
                $("#unlike").prop('disabled', false);
            }
        });
    };
});
$(document).ready(function() {
    // play one audio at a time
    $(function(){
        $("audio").on("play", function() {
            $("audio").not(this).each(function(index, audio) {
                audio.pause();
            });
                /*
                //pause apple music if playing
                setTimeout(function(){
                    $('.pause').addClass('play').removeClass('pause');
                    $('.in-progress').addClass('').removeClass('in-progress');
                    $('.playing').addClass('').removeClass('playing');
                },1);
                */
        });
    });
    
    
    // play one video at a time
    $(function(){
        $('video').on('play', function (e) {
            alert("Uuhm!");
            /*
            var video = $('video');
            for(var i=0;i<video.length;i++)
            {
                if(video[i] != e.target)
                {
                   video[i].pause();
                }
            }
            */
        });
    });

});
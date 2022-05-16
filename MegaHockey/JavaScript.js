    function download(filename, text) {
        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
        element.setAttribute('download', filename);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }


    let games = document.querySelectorAll('.b-wide_tile_item');

    str = 'Хозяева,Гости,Первые,Вторые\n';

    games.forEach(function (game) {

        let names = game.querySelectorAll('.e-club_name a');
        let home = names[0].innerHTML
        let guest = names[1].innerHTML

        let rawscore = game.querySelector('.b-total_score h3')
        let score = rawscore?.innerHTML.replace(/[^0-9]/g, "")
        if (score) {
            let homescore = score[0]
            let guestscore = score[1]
            str += home + ',' + guest + ',' + homescore + ',' + guestscore + '\n';
        }
    });

    download('test.csv', str);
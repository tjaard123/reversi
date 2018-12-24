import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  game = {
    player: 'x',
    board: [
      '--------',
      '--------',
      '--------',
      '---ox---',
      '---xo---',
      '--------',
      '--------',
      '--------',
    ],
    validMoves: ["D3", "C4", "F5", "E6"],
    move: '',
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.move();
  }

  move(x, y) {
    this.game.move = `${String.fromCharCode("A".charCodeAt(0) + x)}${y + 1}`;
    if (this.game.player == 'x' && this.game.validMoves.indexOf(this.game.move) >= 0) {

      this.game.validMoves.splice(0);

      this.http.post('http://localhost:5000/api/game', this.game)
        .toPromise()
        .then((response: any) => {
          this.game = response;
          this.bumblebeeMove();
        })
        .catch((error) => {
          console.error(error);
        });
    } else if (this.game.player == 'o') {
      this.bumblebeeMove();
    }
  }

  bumblebeeMove() {
    this.game.move = this.game.validMoves[Math.floor(Math.random() * this.game.validMoves.length)];
    this.game.validMoves.splice(0);

    this.http.post('http://localhost:5000/api/game', this.game)
      .toPromise()
      .then((response: any) => {
        this.game = response;
      })
      .catch((error) => {
        console.error(error);
      });

    this.game.player = 'x';
  }

  isValidMove(x, y) {
    var pos = `${String.fromCharCode("A".charCodeAt(0) + x)}${y + 1}`;
    return this.game.validMoves.indexOf(pos) >= 0;
  }

  isMove(x, y) {
    var pos = `${String.fromCharCode("A".charCodeAt(0) + x)}${y + 1}`;
    return this.game.move == pos;
  }

  playerScore(player) {
    return (this.game.board.join('').match(new RegExp(player, "g")) || []).length;
  }
}

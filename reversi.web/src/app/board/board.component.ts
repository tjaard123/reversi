import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { DataStorage } from '../data-storage';
import { Player } from '../player';

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

  player1: Player = {
    name: "Player 1",
    avatar: "row-1-col-1.png",
    color: "bg-danger",
    isBot: false
  };

  player2: Player = {
    name: "Player 2",
    avatar: "row-1-col-2.png",
    color: "bg-dark",
    isBot: true
  }

  constructor(private http: HttpClient, private route: ActivatedRoute, private _data: DataStorage) {
    if (_data.player1 && _data.player2) {
      this.player1 = _data.player1;
      this.player2 = _data.player2;
    }
  }

  ngOnInit() {}

  currentPlayerIsBot() {
    return ((this.game.player == 'x' && this.player1.isBot) || (this.game.player == 'o' && this.player2.isBot));
  }

  move(x, y) {
    if (this.currentPlayerIsBot()) {
      this.game.move = this.game.validMoves[Math.floor(Math.random() * this.game.validMoves.length)];
    }
    else {
      this.game.move = `${String.fromCharCode("A".charCodeAt(0) + x)}${y + 1}`;
    }

    if (this.game.validMoves.indexOf(this.game.move) >= 0) {

      this.game.validMoves.splice(0);

      this.http.post('http://localhost:5000/api/game', this.game)
        .toPromise()
        .then((response: any) => {
          this.game = response;

          if (this.currentPlayerIsBot()) {
            this.move(-1, -1);
          }
        })
        .catch((error) => {
          console.error(error);
        });
    }
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

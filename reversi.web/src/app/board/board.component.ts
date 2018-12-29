import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { DataStorage } from '../data-storage';
import { Player } from '../player';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css'],
  animations: [
    trigger('turn', [
      state('-', style({
        transform: 'translateY(500px)',
        opacity: 0
      })),
      state('o', style({
        backgroundColor: 'white'
      })),
      state('x', style({
        transform: 'rotateY(180deg)',
        backgroundColor: 'black'
      })),
      transition('o <=> x', [
        animate('0.4s')
      ]),
      transition('- => *', [
        animate('0.7s')
      ])
    ]),
  ],
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
    turns: [],
    move: '',
  };

  stateBoard = this.game.board.map(y => y.split('').map(x => { return { s: x } }));

  player1: Player = {
    name: "Player 1",
    avatar: "row-1-col-1.png",
    isBot: false
  };

  player2: Player = {
    name: "Player 2",
    avatar: "row-1-col-2.png",
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
      var moveX = this.game.move[0].charCodeAt(0) - "A".charCodeAt(0);
      var moveY = parseInt(this.game.move[1]) - 1;
      this.stateBoard[moveY][moveX].s = this.game.player;

      setTimeout(() => this.refreshBoard(), 1000);
    }
  }

  refreshBoard() {
    this.game.validMoves.splice(0);
    this.game.board = this.stateBoard.map(y => y.map(x => x.s).join(''));

    this.http.post('http://localhost:5000/api/game', this.game)
      .toPromise()
      .then((response: any) => {
        this.game = response;

        this.turn(0);

        if (this.currentPlayerIsBot()) {
          setTimeout(() => this.move(-1, -1), 1000);
        }
      })
      .catch((error) => {
        console.error(error);
      });
  }

  turn(i) {
    if (i < this.game.turns.length) {
      var turn = this.game.turns[i]
      var x = turn[0].charCodeAt(0) - "A".charCodeAt(0);
      var y = parseInt(turn[1]) - 1;
      this.stateBoard[y][x].s = this.game.player == 'x' ? 'o' : 'x';

      setTimeout(() => this.turn(i + 1), 300);
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

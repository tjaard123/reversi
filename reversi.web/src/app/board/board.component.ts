import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  gameState = [
    '--------',
    '--------',
    '--------',
    '---ox---',
    '---xo---',
    '--------',
    '--------',
    '--------',
  ];

  currentPlayer = 'x';
  validMoves = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getValidMoves();
  }

  nextMove(x, y) {
    var v = this.gameState[y].split('');
    v[x] = this.currentPlayer;
    this.gameState[y] = v.join('');
    this.currentPlayer = this.currentPlayer == 'o' ? 'x' : 'o';

    this.getValidMoves();
  }

  getValidMoves() {
    var request = {
      player: this.currentPlayer,
      board: this.gameState
    }

    this.http.post('http://localhost:5000/api/game', request)
      .toPromise()
      .then((response: any) => {
        console.log(response);
        this.validMoves = response;
      })
      .catch((error) => {
        console.error(error);
      });
  }

  isValidMove(x, y) {
    var pos = `${String.fromCharCode("A".charCodeAt(0) + x)}${y + 1}`;
    return this.validMoves.indexOf(pos) >= 0;
  }
}

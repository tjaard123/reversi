import { Component, OnInit } from '@angular/core';

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

  currentPlayer = 'o';
  validMoves = [];

  constructor() { }

  ngOnInit() {
  }

  nextMove(x, y) {
    var v = this.gameState[x].split('');
    v[y] = this.currentPlayer;
    this.gameState[x] = v.join('');
    
    this.currentPlayer = this.currentPlayer == 'o' ? 'x' : 'o';

    this.getValidMoves();
  }

  atN = (x,y) => { return x == 0 ? '-' : this.gameState[x - 1][y]; };
  atNE = (x,y) => { return x == 0 || y == 7 ? '-' : this.gameState[x - 1][y + 1]; };
  atE = (x,y) => { return y == 7 ? '-' : this.gameState[x][y + 1]; };
  atSE = (x,y) => { return x == 7 || y == 7 ? '-' : this.gameState[x + 1][y + 1]; };
  atS = (x,y) => { return x == 7 ? '-' : this.gameState[x + 1][y]; };
  atSW = (x,y) => { return x == 7 || y == 0 ? '-' : this.gameState[x + 1][y - 1]; };
  atW = (x,y) => { return y == 0 ? '-' : this.gameState[x][y - 1]; };
  atNW = (x,y) => { return x == 0 || y == 0 ? '-' : this.gameState[x - 1][y - 1]; };

  getValidMoves() {
    // Tree traversal
    this.validMoves = [];
    this.gameState.forEach((row, x) => {
      row.split('').forEach((cell, y) => {
        if (cell == '-') {
          if (this.atN(x, y) != '-'
            || this.atNE(x, y) != '-'
            || this.atE(x, y) != '-'
            || this.atSE(x, y) != '-'
            || this.atS(x, y) != '-'
            || this.atSW(x, y) != '-'
            || this.atW(x, y) != '-'
            || this.atNW(x, y) != '-') {
              this.validMoves.push(`${x}${y}`);
            }
        }
      })
    })
  }

  isValidMove(x,y) {
    return this.validMoves.indexOf(`${x}${y}`) >= 0;
  }

}

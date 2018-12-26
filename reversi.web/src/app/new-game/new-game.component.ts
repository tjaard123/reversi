import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataStorage } from "../data-storage";
import { Player } from "../player";

@Component({
  selector: 'app-new-game',
  templateUrl: './new-game.component.html',
  styleUrls: ['./new-game.component.css']
})
export class NewGameComponent implements OnInit {

  player1: Player = {
    name: "",
    avatar: "row-1-col-1.png",
    color: "bg-primary",
    isBot: false
  }

  player2: Player = {
    name: "",
    avatar: "row-1-col-2.png",
    color: "bg-danger",
    isBot: true
  }

  constructor(private router: Router, private _data: DataStorage) { }

  ngOnInit() {
  }

  selectAvatar(x, y, player) {
    player.avatar = `row-${y}-col-${x}.png`;
  }

  selectColor(color, player) {
    player.color = `bg-${color}`;
  }

  start() {
    this._data.player1 = this.player1;
    this._data.player2 = this.player2;
    this.router.navigate(['game']);
  }

}

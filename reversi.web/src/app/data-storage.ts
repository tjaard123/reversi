import { Injectable, NgModule } from '@angular/core';
import { Player } from './player';

@NgModule()
@Injectable()
export class DataStorage {
    public player1: Player;
    public player2: Player;
    public constructor() { }
}
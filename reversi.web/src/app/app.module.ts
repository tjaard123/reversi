import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { DataStorage } from './data-storage'; 
import { AppComponent } from './app.component';
import { BoardComponent } from './board/board.component';
import { NewGameComponent } from './new-game/new-game.component';

const appRoutes: Routes = [
  { path: 'game', component: BoardComponent, data: { name: "Hanlu du Plessis" } },
  { path: 'new', component: NewGameComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    BoardComponent,
    NewGameComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes
    ),    
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    DataStorage
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

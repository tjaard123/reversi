import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { BoardComponent } from './board/board.component';

const appRoutes: Routes = [
  { path: 'board', component: BoardComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    BoardComponent
  ],
  imports: [
    RouterModule.forRoot(
      appRoutes
    ),    
    BrowserModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
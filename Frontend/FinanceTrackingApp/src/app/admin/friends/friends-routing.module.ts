import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddfriendComponent } from './addfriend/addfriend.component';
import { FriendsListComponent } from './friends-list/friends-list.component';
import { FriendsComponent } from './friends.component';

const routes: Routes = [
  {
    path: '', component: FriendsComponent, children: [
      {
        path: 'addfriend', component: AddfriendComponent
      },
      {
        path:'', component:FriendsListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FriendsRoutingModule { }

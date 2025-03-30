import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddfriendComponent } from './addfriend/addfriend.component';
import { FriendsListComponent } from './friends-list/friends-list.component';
import { FriendsComponent } from './friends.component';
import { FriendRequestListComponent } from './friend-request-list/friend-request-list.component';

const routes: Routes = [
  {
    path: '', component: FriendsComponent, children: [
      {
        path: 'addfriend', component: AddfriendComponent
      },
      {
        path:'', component:FriendsListComponent
      },
      {
        path: 'friend-list-request', component: FriendRequestListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FriendsRoutingModule { }

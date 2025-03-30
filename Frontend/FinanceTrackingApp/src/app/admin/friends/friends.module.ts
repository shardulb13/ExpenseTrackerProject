import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FriendsRoutingModule } from './friends-routing.module';
import { FriendsComponent } from './friends.component';
import { AddfriendComponent } from './addfriend/addfriend.component';
import { FriendsListComponent } from './friends-list/friends-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FriendRequestListComponent } from './friend-request-list/friend-request-list.component';


@NgModule({
  declarations: [
    FriendsComponent,
    AddfriendComponent,
    FriendsListComponent,
    FriendRequestListComponent
  ],
  imports: [
    CommonModule,
    FriendsRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class FriendsModule { }

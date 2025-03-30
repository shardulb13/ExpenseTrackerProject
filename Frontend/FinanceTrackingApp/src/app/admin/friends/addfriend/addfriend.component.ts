import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { FriendRequestsService } from 'src/app/core/services/friend-requests.service';
import { FriendsService } from 'src/app/core/services/friends.service';

@Component({
  selector: 'app-addfriend',
  templateUrl: './addfriend.component.html',
  styleUrls: ['./addfriend.component.scss']
})
export class AddfriendComponent implements OnInit {
  checkedList: any = [];
  friendsList: any = [];
  tempList: any = [];
  selectedlist: any = [];
  showDropDown!: boolean;
  allUsers: any;
  loggedInUser: any;
  addFriendForm: any;
  constructor(private authService: AuthenticationService, private friendsService: FriendsService, private friendRequestService: FriendRequestsService, private route: Router, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.authService.getAllUsers().subscribe(res => {
      this.allUsers = res;
      this.friendsService.getFriends().subscribe(res => {
        if (res.length == 0) {
          for (let i = 0; i < this.allUsers.length; i++) {
            this.friendsList.push({ 'id': this.allUsers[i].id, 'userName': this.allUsers[i].userName, 'firstName': this.allUsers[i].firstName, 'lastName': this.allUsers[i].lastName });
          }
        }
        else {
          this.tempList = res[0].friendUserId;
          for (let i = 0; i < this.allUsers.length; i++) {
            let matchingUsername = this.tempList.filter((x: any) => x == this.allUsers[i].userName);
            if (matchingUsername != this.allUsers[i].userName) {
              this.friendsList.push({ 'id': this.allUsers[i].id, 'userName': this.allUsers[i].userName, 'firstName': this.allUsers[i].firstName, 'lastName': this.allUsers[i].lastName });
            }
          }
        }
      });
    });

    this.authService.getCurrentUserDetails().subscribe(res => {
      this.loggedInUser = res;
    });

    this.addFriendForm = new FormGroup({
      userId: new FormControl(),
      friendUserId: new FormControl(),
    });


  }

  getSelectedValue(status: Boolean, value: String, id: string) {
    if (status) {
      this.checkedList.push(id);
      this.selectedlist.push(value);
    } else {
      var index = this.checkedList.indexOf(value);
      var index1 = this.selectedlist.indexOf(value);
      this.checkedList.splice(index, 1);
      this.selectedlist.splice(index1, 1);
    }
  }

  mouseleavefunc(e: any) {
    this.showDropDown = false;
  }

  addFriendRequest() {
    if (this.checkedList.length == 0) {
      alert("Select User");
    }
    else {
      this.addFriendForm.controls['userId'].setValue(this.loggedInUser.id);
      this.addFriendForm.controls['friendUserId'].setValue(this.checkedList);

      this.friendRequestService.addFriendRequest(this.addFriendForm.value).subscribe(res => {
        this.toastrService.success("Friend Request sent successfully");
        this.route.navigate(['user/friends']);
      },
        err => {
          this.toastrService.error("Error in adding friend");
        });
    }
  }

}

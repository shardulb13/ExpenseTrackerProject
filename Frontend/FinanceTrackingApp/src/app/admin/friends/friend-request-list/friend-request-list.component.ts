import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FriendRequestsService } from 'src/app/core/services/friend-requests.service';

@Component({
  selector: 'app-friend-request-list',
  templateUrl: './friend-request-list.component.html',
  styleUrls: ['./friend-request-list.component.scss']
})
export class FriendRequestListComponent implements OnInit {
 TempGetFriendsList: any = [];
  friendsList: any = [];
  acceptButtonFlag: boolean = false;

  constructor(private friendRequestService: FriendRequestsService, private toastrService: ToastrService,  private route: Router) {
  }

  ngOnInit(): void {
    this.friendRequestService.getFriendsData().subscribe(res => {
      this.TempGetFriendsList = res;
      this.TempGetFriendsList.forEach((item:any) => {
      if(item.userId == item.sentby){

        this.acceptButtonFlag = true;
        console.log("flag value", this.acceptButtonFlag);
      }
     });
      console.log(this.TempGetFriendsList);
    });

    // this.friendRequestService.getFriendsData().subscribe(res => {
    //   this.tempfriendlist = res;
    // })

  }

  delete(id: any) {
    this.friendRequestService.deleteFriend(id).subscribe(res => {
      this.toastrService.error("Friend Request Deleted Successfully");
      this.ngOnInit();
    },
      err => {
        this.toastrService.warning("Error in deleting friend request");
      })
  }

  acceptFriendRequest(data: any) {
    console.log("Add Friend Data", data);

    if (data?.length == 0 && data ==null && data == undefined ) {
      alert("Something went wrong");
    }
    else {
      console.log("Calling service");
      this.friendRequestService.acceptFriendRequest(data).subscribe(res => {
        this.toastrService.success("Friend Added Successfully");
        this.route.navigate(['user/friends']);
      },
        err => {
          this.toastrService.error("Error in adding friend");
        });
    }
  }
}

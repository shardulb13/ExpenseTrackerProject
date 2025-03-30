import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { FriendsService } from 'src/app/core/services/friends.service';
import { GroupsService } from 'src/app/core/services/groups.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  selectedlist:any=[];
  allUsers:any;
  tempFriendsList:any;
  allFriends:any =[];
  checkedList : any=[];
  showDropDown!:boolean;
  groupForm:any;
  loggedInUser:any;
  submitted:boolean = false;
  constructor(private authService: AuthenticationService, private groupService: GroupsService, private toastrService: ToastrService, private route: Router,
    private friendService: FriendsService) { }

  ngOnInit(): void {
    this.groupForm = new FormGroup({
      groupName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
      userId: new FormControl(this.checkedList)
    });

    this.authService.getAllUsers().subscribe(res =>{
      this.allUsers = res;
      this.friendService.getFriends().subscribe(res=>{
        this.tempFriendsList = res[0].friendUserId;
        for(let i= 0; i<this.allUsers.length; i++){
          let matchingUsername = this.tempFriendsList.filter((x:any) => x == this.allUsers[i].userName);
          if(this.allUsers[i].userName == matchingUsername){
            this.allFriends.push({'id': this.allUsers[i].id, 'userName':this.allUsers[i].userName});
          }
        }
      });
    });

    this.authService.getCurrentUserDetails().subscribe(res=>{
      this.loggedInUser = res;
      this.checkedList.push( this.loggedInUser.id);
      console.log(this.checkedList);
    })

  }

  getSelectedValue(status:Boolean,value:String, id:string){
    if(status){
      this.checkedList.push(id);
      this.selectedlist.push(value);  
    }else{
        var index = this.checkedList.indexOf(value);
        var index1 = this.selectedlist.indexOf(value);
        this.checkedList.splice(index,1);
        this.selectedlist.splice(index1,1);
    }
    // this.currentSelected = {checked : status,name:value};
  }
  mouseleavefunc(e:any){
    this.showDropDown = false;
  }

  createGroup(){
    if(this.checkedList.length < 2){
      alert("Select Friends");
    }
    else{
      this.groupService.addGroup(this.groupForm.value).subscribe(res=>{
        console.log(res);
        this.toastrService.success("Group Created Successfully");
        console.log(this.groupForm.value);
        this.route.navigate(['user/groups']);
      },
      err=>{
        this.toastrService.error("Group Creation Failed");
      });
    }
    console.log(this.groupForm.value);
  }

  get form(){
    return this.groupForm.controls;
  }

}

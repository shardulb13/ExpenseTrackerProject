import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
// import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { ExpenseService } from 'src/app/core/services/expense.service';
import { FriendsService } from 'src/app/core/services/friends.service';
import { GroupsService } from 'src/app/core/services/groups.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  expform!: any;
  currentUserDetails!: any;
  allUsers: any = [];
  tempFriendList: any = [];
  friendList: any = [];
  selectedlist: any = [];
  checkedList: any = [];
  showDropDown!: boolean;
  date!: string;
  usersGroup: any = [];
  paidBylist: any = [];
  isdisable = false;
  btnDisable:boolean = true;
  constructor(private authService: AuthenticationService, private expenseService: ExpenseService, private router: Router, private toastrService: ToastrService,
    private groupService: GroupsService, private friendService: FriendsService) { }

  ngOnInit(): void {
    this.expform = new FormGroup({
      ExpenseName: new FormControl('', [Validators.required]),
      ExpenseDate: new FormControl('', [Validators.required]),
      Amount: new FormControl('', [Validators.required, Validators.pattern("^[0-9]*$")]),
      PaidBy: new FormControl('', [Validators.required]),
      GroupId: new FormControl(null),
      IsActive: new FormControl(true),
      UserId: new FormControl([])
    });

    this.authService.getCurrentUserDetails().subscribe(res => {
      this.currentUserDetails = res;
      this.checkedList.push(this.currentUserDetails.id);
      this.paidBylist.push({ 'id': this.currentUserDetails.id, 'userName': this.currentUserDetails.userName })
    });

    this.authService.getAllUsers().subscribe(res => {
      this.allUsers = res;
      this.friendService.getFriends().subscribe(res => {
        this.tempFriendList = res[0].friendUserId;
        for (let i = 0; i < this.allUsers.length; i++) {
          let matchingUserName = this.tempFriendList.filter((x: any) => x == this.allUsers[i].userName);
          if (this.allUsers[i].userName == matchingUserName) {
            this.friendList.push({
              'id': this.allUsers[i].id,
              'userName': this.allUsers[i].userName
            });
          }
        }
      })

    });

    this.date = new Date().toISOString().slice(0, 10);
    this.groupService.getGroups().subscribe(res => {
      this.usersGroup = res;
    })
  }

  get form() {
    return this.expform.controls;
  }

  addExpense() {
    if(this.expform.valid){
      if (this.expform.controls['GroupId'].value == null) {
        this.expform.controls['UserId'].setValue(this.checkedList);
        this.expenseService.AddExpenses(this.expform.value).subscribe(res => {
          this.toastrService.success("Expenes Added Successfully");
          this.router.navigate(['user/allexpenses']);
        },
          err => {
            this.toastrService.error("Something went wrong");
          });
      }
      else {
        this.expenseService.AddExpenses(this.expform.value).subscribe(res => {
          this.toastrService.success("Expenes Added Successfully");
          this.router.navigate(['user/groups']);
        },
          err => {
            this.toastrService.error("Something went wrong");
          });
      }
    }
    else{
      alert("Please fill up the expense details");
    }
  }

  getSelectedValue(status: Boolean, value: String, id: string) {
    if (status) {
      this.checkedList.push(id);
      this.selectedlist.push(value);
      this.paidBylist.push({ 'id': id, 'userName': value });
    } else {
      var index = this.checkedList.indexOf(value);
      var index1 = this.selectedlist.indexOf(value);
      this.checkedList.splice(index, 1);
      this.selectedlist.splice(index1, 1);
      this.paidBylist.splice(index, 1);
    }
  }

  mouseleavefunc(e: any) {
    this.showDropDown = false;
  }

  onchange() {
    // this.paidBylist = [];
    this.checkedList = [];
    if (this.expform.controls['GroupId'].value > 0) {
      for (let i = 0; i < this.usersGroup.length; i++) {
        if (this.usersGroup[i].id == this.expform.controls['GroupId'].value) {
          for (let j = 0; j < this.usersGroup[i].userId.length; j++) {
            for (let k = 0; k < this.allUsers.length; k++) {
              let matchingUserName = this.usersGroup[i].userId.filter((x: any) => x == this.allUsers[k].userName);
              if (this.allUsers[k].userName == matchingUserName) {
                this.paidBylist.push({
                  'id': this.allUsers[k].id,
                  'userName': this.allUsers[k].userName
                });
              }
            }
            break;
          }
        }
      }
    }
  }

  reset(){
    location.reload();
  }

}

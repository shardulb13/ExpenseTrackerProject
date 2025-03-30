import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { ExpenseService } from 'src/app/core/services/expense.service';
import { GroupsService } from 'src/app/core/services/groups.service';

@Component({
  selector: 'app-group-expenses',
  templateUrl: './group-expenses.component.html',
  styleUrls: ['./group-expenses.component.scss']
})
export class GroupExpensesComponent implements OnInit {
  pageNo = 1;
  allGroups: any = [];
  groupExpenses: any = [];
  id!: any;
  loggedInUser: any;
  result = 0;
  oweTemp = 0;
  settleTemp = 0;
  oweAmount = 0;
  settleAmount = 0;
  arrayofowe: any = [];
  arrayofSettle: any = [];
  showOweAmount: any = [];
  showSettleAmount: any = [];


  constructor(private groupService: GroupsService, private expenseService: ExpenseService, private activatedRoute: ActivatedRoute, private authService: AuthenticationService,
    private toastrService: ToastrService, private route: Router) { }

  ngOnInit(): void {
    this.groupService.getGroups().subscribe(res => {
      this.allGroups = res;
    });

    this.id = this.activatedRoute.snapshot.paramMap.get('id');

    this.expenseService.getExpensebyGroup(this.id).subscribe(res => {
      this.groupExpenses = res;
      this.authService.getCurrentUserDetails().subscribe(res => {
        this.loggedInUser = res;
        for (let i = 0; i <= this.groupExpenses.length; i++) {
          let tempResult = this.groupExpenses[i].amount;
          this.result += Math.round(tempResult);
          if (this.groupExpenses[i].paidBy.toLowerCase() == this.loggedInUser.userName.toLowerCase()) {
            this.oweTemp = (this.groupExpenses[i].amount) / this.groupExpenses[i].userId.length;
            this.oweAmount += Math.round(this.oweTemp) * (this.groupExpenses[i].userId.length - 1);
            this.arrayofowe = this.groupExpenses[i].userId;

            for (let j = 0; j < this.arrayofowe.length; j++) {
              if (this.arrayofowe[j].toLowerCase() != this.loggedInUser.userName.toLowerCase()) {
                this.showOweAmount.push({ 'userName': this.arrayofowe[j], 'amount': Math.round(this.oweTemp), 'ExpenseName': this.groupExpenses[i].expenseName });
              }
            }
          }
          else {
            this.settleTemp = (this.groupExpenses[i].amount) / this.groupExpenses[i].userId.length;
            this.settleAmount += Math.round(this.settleTemp);
            this.arrayofSettle = this.groupExpenses[i].userId;
            for (let j = 0; j < this.arrayofSettle.length; j++) {
              if (this.groupExpenses[i].paidBy == this.arrayofSettle[j]) {
                this.showSettleAmount.push({ 'userName': this.arrayofSettle[j], 'amount': Math.round(this.settleTemp), 'ExpenseName': this.groupExpenses[i].expenseName });
              }
            }
          }
        }

      });
    });
  }

  deleteGroupExpense(id: number) {
    this.expenseService.DeleteExpense(id).subscribe(res => {
      this.toastrService.error("Expense Deleted Successfully");
      window.location.reload();
    },
      err => {
        this.toastrService.warning("Error in deleting expense");
      });
  }

  editExpense(id: number) {
    this.route.navigate([`user/allexpenses/edit/${id}`]);
  }
}

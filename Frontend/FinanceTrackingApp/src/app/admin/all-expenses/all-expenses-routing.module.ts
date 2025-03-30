import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from './add/add.component';
import { AllExpenseComponent } from './all-expense.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  {
    path:'', component: AllExpenseComponent, children:[
      {
        path:'add', component: AddComponent
      },
      {
        path:'', component: ListComponent
      },
      {
        path:'edit/:id', component:EditComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AllExpensesRoutingModule { }

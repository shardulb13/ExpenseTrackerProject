import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AllExpensesRoutingModule } from './all-expenses-routing.module';
import { AllExpenseComponent } from './all-expense.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';


@NgModule({
  declarations: [
    AllExpenseComponent,
    AddComponent,
    EditComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    AllExpensesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    // NgSelectModule,
  ]
})
export class AllExpensesModule { }

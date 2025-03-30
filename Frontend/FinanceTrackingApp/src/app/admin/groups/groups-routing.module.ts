import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { GroupExpensesComponent } from './group-expenses/group-expenses.component';
import { GroupsComponent } from './groups.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [
  {
    path:'', component:GroupsComponent, children:[
      {
        path:'addGroup', component: AddComponent
      },
      {
        path:'editGroup/:id', component:EditComponent
      },
      {
        path:'', component:ListComponent
      },
      {
        path:'groupexpenses/:id', component:GroupExpensesComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GroupsRoutingModule { }

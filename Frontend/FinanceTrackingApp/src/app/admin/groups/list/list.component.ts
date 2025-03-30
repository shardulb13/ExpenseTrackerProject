import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GroupsService } from 'src/app/core/services/groups.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  listOfGroups:any=[];
  constructor(private groupService: GroupsService, private route: Router, private toastrService: ToastrService) { }

  ngOnInit(): void {

    this.groupService.getGroups().subscribe(result=>{
      this.listOfGroups = result;
    })
  }

  deleteGroup(id:number){
    this.groupService.deleteGroup(id).subscribe(del=>{
      this.toastrService.error("Group Deleted Successfully");
      this.ngOnInit();
    },
    err=>{
      this.toastrService.error("Error in deleting group");
    })
  }

  edit(id:number){
    this.route.navigate([`user/groups/editGroup/${id}`]);
  }
  view(id:number){
    this.route.navigate([`user/groups/groupexpenses/${id}`]);
  }
}

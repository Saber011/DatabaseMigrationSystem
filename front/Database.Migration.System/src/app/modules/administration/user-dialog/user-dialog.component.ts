import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../../api/services/user.service";
import {UserDto} from "../../../api/models/user-dto";

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialog implements OnInit {
  displayedColumns: string[] = ['id', 'login', 'action'];
  userName: string | null | undefined;

  dataSource!: MatTableDataSource<UserDto>;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(public dialogRef: MatDialogRef<UserDialog>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private readonly userService: UserService,
              public dialog: MatDialog) { }

  ngOnInit(): void {
    this.refreshData();
  }

  refreshData(){
    this.userService.apiUserGetUserByIdGet({UserId: this.data.id})
      .subscribe(value => {
        if(value) {
          this.userName = value.login;
        }
      });
  }


  updateRowData(){
    this.userService.apiUserUpdateUserInfoPost$Response({body:{
        userId: this.data.id,
        login: this.userName
      }}).subscribe()
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}

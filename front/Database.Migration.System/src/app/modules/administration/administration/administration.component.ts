import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {MatSort} from "@angular/material/sort";
import {MatDialog} from "@angular/material/dialog";
import {UserService} from "../../../api/services/user.service";
import {UserDto} from "../../../api/models/user-dto";
import {UserDialog} from "../user-dialog/user-dialog.component";

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css']
})
export class AdministrationComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'login', 'edit', 'delete'];
  dataSource!: MatTableDataSource<UserDto>;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly userService: UserService, public dialog: MatDialog) {
  }

  ngOnInit(){
    this.refreshData();
  }

  private refreshData() {
    this.userService.apiUserGetAllUsersGet()
      .subscribe(value => {
        if(value) {
          this.dataSource = new MatTableDataSource(value);
          this.dataSource.sort = this.sort;
        }

      });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openDialog(userId: number): void {
    const dialogRef = this.dialog.open(UserDialog, {
      width: '80%',
      data: {
        id: userId,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      });
  }


  deleteUser(userId: number): void {
    this.userService.apiUserDeleteUserPost$Response({body: {userId: userId}})
      .subscribe(value => this.refreshData());
  }

}

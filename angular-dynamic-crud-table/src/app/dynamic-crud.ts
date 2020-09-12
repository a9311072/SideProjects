import {Component, OnInit, ViewChild} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {MatDialogRef,MatDialog} from '@angular/material';
import {ConfirmationDialog} from './confirmation-dialog.component';
/**
 * @title Table with pagination
 */
@Component({
  selector: 'dynamic-crud',
  styleUrls: ['dynamic-crud.css'],
  templateUrl: 'dynamic-crud.html',
})
export class DynamicCrud {
  
  constructor(private dialog: MatDialog) {}
  columns = [
    { columnDef: 'position', header: 'Id',    cell: (element: any) => `${element.position}` },
    { columnDef: 'name',     header: 'Name',   cell: (element: any) => `${element.name}`     },
    { columnDef: 'weight',   header: 'Weight', cell: (element: any) => `${element.weight}`   },
    { columnDef: 'symbol',   header: 'Symbol', cell: (element: any) => `${element.symbol}`   },

  ];

  displayedColumns = this.columns.map(c => c.columnDef).concat(['Actions']);
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
  
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  
  ngOnInit() {
    this.paginator._intl.itemsPerPageLabel = 'Rows';
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  add() {
    console.log(Object.values(this.columns));
    for (let column of this.columns) {
      console.log(column.header); // 1, "string", false
    }

    
  }

  edit(id: number, row: string[]){
    console.log(row);
    console.log(Object.values(row));

    for (let entry in row) {
      console.log(entry); // 1, "string", false
    }
    for (let entry of Object.values(row)) {
      console.log(entry); // 1, "string", false
    }
  }

  delete(row: string[]) {
    this.dataSource.data.splice(ELEMENT_DATA.indexOf(row), 1);
    this.dataSource._updateChangeSubscription()
  }

  open() {
    // const dialog = new MatDialog();
    const dialogRef = this.dialog.open(ConfirmationDialog,{
      data:{
        message: 'Are you sure want to delete?',
        buttonText: {
          ok: 'Save',
          cancel: 'No'
        }
      }
    });
  }
}



export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
  {position: 11, name: 'Sodium', weight: 22.9897, symbol: 'Na'},
  {position: 12, name: 'Magnesium', weight: 24.305, symbol: 'Mg'},
  {position: 13, name: 'Aluminum', weight: 26.9815, symbol: 'Al'},
  {position: 14, name: 'Silicon', weight: 28.0855, symbol: 'Si'},
  {position: 15, name: 'Phosphorus', weight: 30.9738, symbol: 'P'},
  {position: 16, name: 'Sulfur', weight: 32.065, symbol: 'S'},
  {position: 17, name: 'Chlorine', weight: 35.453, symbol: 'Cl'},
  {position: 18, name: 'Argon', weight: 39.948, symbol: 'Ar'},
  {position: 19, name: 'Potassium', weight: 39.0983, symbol: 'K'},
  {position: 20, name: 'Calcium', weight: 40.078, symbol: 'Ca'},
  {position: 21, name: 'Zodium', weight: 32.9897, symbol: 'Zo'},
];



/**  Copyright 2019 Google LLC. All Rights Reserved.
    Use of this source code is governed by an MIT-style license that
    can be found in the LICENSE file at http://angular.io/license */
import {
  Component,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  AfterViewInit,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface TableColumn<T = unknown> {
  key: string;
  header: string;
  sortable?: boolean;
  cell?: (row: T) => string;
}

export interface TableAction<T = unknown> {
  icon: string;
  label: string;
  type: 'edit' | 'delete' | string;
  emit: (row: T) => void;
}

@Component({
  selector: 'app-reusable-table',
  standalone: false,
  templateUrl: './reusable-table.component.html',
  styleUrls: ['./reusable-table.component.scss'],
})
export class ReusableTableComponent implements AfterViewInit {
  @Input() columns: TableColumn<any>[] = [];
  @Input() actions: TableAction<any>[] = [];
  @Input() pageSizeOptions = [5, 10, 25, 50];
  @Input() loading = false;

  @Output() rowEdit = new EventEmitter<unknown>();
  @Output() rowDelete = new EventEmitter<unknown>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  dataSource = new MatTableDataSource<any>([]);
  displayedColumns: string[] = [];

  @Input() set data(rows: any[] | null) {
    this.dataSource.data = rows ?? [];
    this.updateDisplayedColumns();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getColumnKey(col: TableColumn): string {
    return col.key;
  }

  private updateDisplayedColumns(): void {
    const colKeys = this.columns.map((c) => c.key);
    this.displayedColumns = this.actions.length ? [...colKeys, 'actions'] : colKeys;
  }

  getCellValue(row: any, column: TableColumn): string {
    if (column.cell) {
      return column.cell(row);
    }
    const value = row[column.key];
    return value != null ? String(value) : '';
  }

  onActionClick(action: TableAction, row: any): void {
    action.emit(row);
  }
}

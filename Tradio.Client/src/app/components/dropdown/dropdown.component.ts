import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Component({
  selector: 'dropdown',
  templateUrl: './dropdown.component.html',
  imports: [CommonModule],
})
export class DropdownComponent {

  @Input() loadData: () => Observable<any[]> = () => of([]);
  @Input() loadSubData: (id: number) => Observable<any[]> = () => of([]);
  @Input() selectedData$!: BehaviorSubject<any>;
  subData$: Observable<any[]> = of([]);
  data$: Observable<any[]> = of([]);
  hoverDataId?: number;

  removeData() {
    this.data$ = of([]);
    this.subData$ = of([]);
  }

  chooseSubdata(subData: any) {
    this.selectedData$.next(subData);
    this.removeData();
  }

  mouseEnterDropdown() {
    this.data$ = this.loadData();
  }

  mouseEnterData(id: number) {
    this.hoverDataId = id;
    this.subData$ = this.loadSubData(id);
  }

  mouseLeaveData() {
    this.hoverDataId = undefined;
    this.removeData();
  }
}

import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';

@Component({
  selector: 'dropdown',
  templateUrl: './dropdown.component.html',
  imports: [CommonModule],
})
export class DropdownComponent {
  @Output() loadData = new EventEmitter<void>();
  @Output() loadSubData = new EventEmitter<number>();
  @Input() selectedData$!: BehaviorSubject<any>;
  @Input() subData$!: Observable<any[]>;
  @Input() data$!: Observable<any[]>;
  hoverDataId?: number;

  removeData() {
    this.data$ = of([]);
    this.subData$ = of([]);
  }

  chooseSubdata(subData: any) {
    this.selectedData$.next(subData);
    this.removeData();
  }

  mouseEnterData(id: number) {
    this.hoverDataId = id;
    this.loadSubData.emit(id);
  }

  mouseLeaveData() {
    this.hoverDataId = undefined;
    this.removeData();
  }
}

import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { DropdownItemModel } from './dropdown-item.model';

@Component({
  selector: 'dropdown',
  templateUrl: './dropdown.component.html',
  imports: [CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropdownComponent),
      multi: true,
    },
  ],
})
export class DropdownComponent implements ControlValueAccessor {
  writeValue(obj: DropdownItemModel): void {
    this.value = obj;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouch = fn;
  }

  @Input() placeholder!: string;
  @Input() loadData: () => Observable<DropdownItemModel[]> = () => of([]);
  @Input() loadSubData: (id: number) => Observable<DropdownItemModel[]> = () =>
    of([]);
  value?: DropdownItemModel;
  private onChange: (value: number) => void = () => {};
  private onTouch: () => void = () => {};
  subData$: Observable<DropdownItemModel[]> = of([]);
  data$: Observable<DropdownItemModel[]> = of([]);
  hoverDataId?: number;

  removeData() {
    this.data$ = of([]);
    this.subData$ = of([]);
  }

  chooseSubdata(subData: DropdownItemModel) {
    this.value = subData;
    this.onChange(this.value.id);
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

import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";

@Component({
    selector: 'services',
    templateUrl: './services.component.html',
    host: {
      class: 'flex'
    },
    imports: [
        CommonModule
    ]
})

export class ServicesComponent{

}
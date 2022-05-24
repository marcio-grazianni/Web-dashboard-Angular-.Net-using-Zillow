import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SystemComponent } from './components/system/system.component';
import { ApifyComponent } from './components/apify/apify.component';
import { ProcessSchedulerComponent } from './components/process-scheduler/process-scheduler.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [SystemComponent, ApifyComponent, ProcessSchedulerComponent],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class TasksModule { }

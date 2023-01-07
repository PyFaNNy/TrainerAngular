import {Patient} from "./patient";

export class Examination {
  id : string = '';
  patientId? : string;
  typePhysicalActive?: string;
  date: Date = new Date();
  indicators: number = 0;
  status? : number;
  patient?: Patient;
}

import {Patient} from "./patient";

export class Examination {
  id? : string;
  patientId? : string;
  typePhysicalActive?: number;
  date: Date = new Date();
  indicators: number = 0;
  status? : number;
  patient?: Patient;
}

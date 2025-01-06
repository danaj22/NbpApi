import DatePicker from "react-datepicker";
import { registerLocale } from "react-datepicker";
import { pl } from "date-fns/locale/pl";
registerLocale("pl", pl);
import "./UpdateDatePicker.css";

interface UpdateDatePickerProps {
  selectedTable: string;
  selectedDate?: Date;
  handleDateSelection: (tableName: string, date: Date) => void;
  availableDates: Date[];
}

const UpdateDatePicker = (props: UpdateDatePickerProps) => {
  return (
    <div>
      Tabela kursów NBP z dnia:
      <DatePicker
        className="dateInput"
        locale="pl"
        selected={props.selectedDate}
        onChange={(date) =>
          date && props.handleDateSelection(props.selectedTable, date)
        }
        includeDates={props.availableDates}
        dateFormat="dd-MM-yyyy"
      />
    </div>
  );
};

export default UpdateDatePicker;

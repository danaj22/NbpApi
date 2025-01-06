import TableSwitch from "../TableSwitch/TableSwitch";
import "./TableSwitcher.css";

export interface TableSwitcherProps {
  selectedTableName: string;
  handleSwitchTable: (tableName: string) => void;
}

const TableSwitcher = (props: TableSwitcherProps) => {
  return (
    <div className="wrapper">
      <TableSwitch
        selectedTableName={props.selectedTableName}
        handleSwitchTable={props.handleSwitchTable}
        tableName="A"
      />
      <TableSwitch
        selectedTableName={props.selectedTableName}
        handleSwitchTable={props.handleSwitchTable}
        tableName="B"
      />
    </div>
  );
};

export default TableSwitcher;

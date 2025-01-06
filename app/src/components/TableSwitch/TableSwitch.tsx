import classNames from "classnames";
import "./TableSwitch.css";

interface TableSwitchProps {
  selectedTableName: string;
  handleSwitchTable: (tableName: string) => void;
  tableName: string;
}

const TableSwitch = (props: TableSwitchProps) => {
  return (
    <a
      className={classNames(
        `nav-link ${
          props.selectedTableName === props.tableName ? "active" : ""
        } `
      )}
      onClick={() => props.handleSwitchTable(props.tableName)}
    >
      <span>{`Tabela ${props.tableName}`}</span>
    </a>
  );
};

export default TableSwitch;

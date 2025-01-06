interface ColumnItemProps {
  sort: (column: string) => void;
  order: string;
  sortBy: string;
  name: string;
  sortingKey: string;
}

const ColumnItem = (props: ColumnItemProps) => {
  return (
    <th onClick={() => props.sort(props.sortingKey)}>
      {props.name}{" "}
      {props.sortBy === props.sortingKey && (props.order === "asc" ? "↑" : "↓")}
    </th>
  );
};

export default ColumnItem;

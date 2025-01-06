import { IRate } from "../ExchangeRatesTable/ExchangeRatesTable";

const ExchangeRate = (props: IRate) => {
  return (
    <tr key={props.id}>
      <td>{props.id}</td>
      <td>{props.name}</td>
      <td>{props.rate}</td>
    </tr>
  );
};

export default ExchangeRate;

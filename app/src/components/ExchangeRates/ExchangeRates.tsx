import { TailSpin } from "react-loader-spinner";
import ColumnItem from "../ColumnItem";
import ExchangeRate from "../ExchangeRate/ExchangeRate";
import { IRate } from "../ExchangeRatesTable/ExchangeRatesTable";

import "./ExchangeRates.css";

interface ExchangeRatesProps {
  isLoading: boolean;
  sortedData: IRate[];
  errorMessage: string;
  handleSort: (column: string) => void;
  sortOrder: string;
  sortBy: string;
}

const ExchangeRates = (props: ExchangeRatesProps) => {
  return (
    <table className="container">
      <thead>
        <tr className="columnTitle">
          <ColumnItem
            sort={props.handleSort}
            order={props.sortOrder}
            sortBy={props.sortBy}
            sortingKey="id"
            name="Kod waluty"
          />
          <ColumnItem
            sort={props.handleSort}
            order={props.sortOrder}
            sortBy={props.sortBy}
            sortingKey="name"
            name="Nazwa"
          />
          <ColumnItem
            sort={props.handleSort}
            order={props.sortOrder}
            sortBy={props.sortBy}
            sortingKey="rate"
            name="Kurs średni"
          />
        </tr>
      </thead>
      <tbody>
        {props.errorMessage ? (
          <tr>
            <td colSpan={3}>{props.errorMessage}</td>
          </tr>
        ) : props.sortedData && props.sortedData?.length > 0 ? (
          props.sortedData.map((rate) => (
            <ExchangeRate {...rate} key={rate.id} />
          ))
        ) : (
          <tr>
            <td colSpan={3} className="loader">
              <TailSpin
                visible={props.isLoading}
                height="80"
                width="80"
                color="#4fa94d"
                ariaLabel="tail-spin-loading"
                radius="1"
              />
            </td>
          </tr>
        )}
      </tbody>
    </table>
  );
};

export default ExchangeRates;

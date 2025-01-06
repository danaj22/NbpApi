import { useEffect, useState } from "react";
import { AxiosError } from "axios";
import "react-datepicker/dist/react-datepicker.css";
import "./ExchangeRatesTable.css";

import {
  getAvailableDates,
  getLatestRates,
  getRatesByDate,
} from "../../services/currencyService";
import Header from "../Header/Header";
import TableSwitcher from "../TableSwitcher/TableSwitcher";
import UpdateDatePicker from "../UdpateDatePicker/UpdateDatePicker";
import SearchInput from "../SearchInput/SearchInput";
import ExchangeRates from "../ExchangeRates/ExchangeRates";
import ErrorMessages from "../../ErrorMessages";

interface ICurrency {
  effecitveDate: string;
  exchangeRates: IRate[];
}

export interface IRate {
  id: string;
  name: string;
  rate: number;
}

const ExchangeRatesTable = () => {
  const defaultTable = "A";

  const [rateTable, setRateTable] = useState<ICurrency | null>();
  const [selectedDate, setSelectedDate] = useState<Date>();
  const [searchTerm, setSearchTerm] = useState("");
  const [sortBy, setSortBy] = useState<string>("id");
  const [sortOrder, setSortOrder] = useState("asc");
  const [errorMessage, setErrorMessage] = useState("");
  const [selectedTableName, setSelectedTableName] = useState(defaultTable);
  const [availableDates, setAvailableDates] = useState<Date[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    fetchRates(defaultTable);
    fetchAvailableDates(defaultTable);
  }, []);

  const fetchRates = async (tableName: string, date?: string) => {
    setIsLoading(true);
    if (date == null) {
      await getLatestRates(tableName)
        .then((response) => {
          if (response.data.exchangeRates.length == 0)
            setErrorMessage(ErrorMessages.NO_RATES);
          else {
            setRateTable(response.data);
            setSelectedDate(new Date(response.data.effecitveDate));
          }
        })
        .catch((error) => {
          handleError(error.code);
        });
    } else {
      await getRatesByDate(tableName, date)
        .then((response) => {
          if (response.data.exchangeRates.length == 0)
            setErrorMessage(ErrorMessages.NO_RATES);
          else {
            setRateTable(response.data);
            setErrorMessage("");
          }
        })
        .catch((error) => {
          handleError(error.code);
        });
    }
  };

  const handleError = (code: string) => {
    if (code === AxiosError.ERR_NETWORK) {
      setErrorMessage(ErrorMessages.SERVER_NOT_RESPOND);
      setRateTable(null);
    }
  };

  const fetchAvailableDates = async (tableName: string) => {
    await getAvailableDates(tableName)
      .then((response) => {
        if (response.data.length == 0) setErrorMessage(ErrorMessages.NO_RATES);
        else {
          setAvailableDates(response.data);
        }
      })
      .catch((error) => {
        handleError(error.code);
      });
  };

  const handleDateSelect = (tableName: string, date: Date) => {
    setIsLoading(true);
    fetchRates(tableName, date.toDateString());
    setSelectedDate(date);
  };

  const filteredData =
    rateTable &&
    rateTable.exchangeRates.filter(
      (item) =>
        item.id.toLowerCase().includes(searchTerm.toLowerCase()) ||
        item.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

  const handleSort = (key: string) => {
    if (sortBy === key) {
      setSortOrder(sortOrder === "asc" ? "desc" : "asc");
    } else {
      setSortBy(key);
      setSortOrder("asc");
    }
  };

  const sortedData =
    filteredData &&
    [...filteredData].sort((a, b) => {
      const aProperty = a[sortBy as keyof IRate];
      const bProperty = b[sortBy as keyof IRate];

      const aValue =
        typeof aProperty === "string" ? aProperty.toLowerCase() : aProperty;
      const bValue =
        typeof bProperty === "string" ? bProperty.toLowerCase() : bProperty;

      if (sortOrder === "asc") {
        return aValue < bValue ? -1 : aValue > bValue ? 1 : 0;
      } else {
        return bValue < aValue ? -1 : bValue > aValue ? 1 : 0;
      }
    });

  const handleSwitchTable = (tableName: string) => {
    fetchAvailableDates(tableName);
    setSelectedTableName(tableName);
    handleDateSelect(tableName, selectedDate!);
  };

  return (
    <div>
      <Header />
      <div className="navigation">
        <UpdateDatePicker
          selectedTable={selectedTableName}
          selectedDate={selectedDate}
          handleDateSelection={handleDateSelect}
          availableDates={availableDates}
        />
        <SearchInput term={searchTerm} search={setSearchTerm} />
        <TableSwitcher
          selectedTableName={selectedTableName}
          handleSwitchTable={handleSwitchTable}
        />
      </div>
      <div className="dataContainer">
        <ExchangeRates
          isLoading={isLoading}
          sortedData={sortedData!}
          errorMessage={errorMessage}
          handleSort={handleSort}
          sortOrder={sortOrder}
          sortBy={sortBy}
        />
      </div>
    </div>
  );
};

export default ExchangeRatesTable;

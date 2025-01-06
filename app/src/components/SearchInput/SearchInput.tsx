interface SearchInputProps {
  term: string;
  search: (value: string) => void;
}

const SearchInput = (props: SearchInputProps) => {
  return (
    <input
      type="text"
      placeholder="Podaj nazwę lub kod"
      value={props.term}
      onChange={(e) => props.search(e.target.value)}
      className="mb-4 p-2 border border-gray-300 rounded"
    />
  );
};

export default SearchInput;

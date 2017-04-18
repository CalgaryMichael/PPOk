UPDATE store
SET address = @address, name = @name, city = @city, state = @state, zip = @zip
WHERE store_id = @store_id
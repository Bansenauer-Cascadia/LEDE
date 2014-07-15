<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LEDE_Entity._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<section>
		<p>Here is a paragraph of filler text. Here, have a <a href="#">test link</a>. This is <a href="#" target="_blank">another test link</a> that opens in a new tab. For text, here is <em>emphasized text</em> and <strong>strong text</strong></p>
		
		<blockquote>This is a blockquote. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque porttitor elit neque, vel suscipit purus vulputate eu. Aliquam commodo quam nec nisl semper, nec facilisis quam volutpat. Sed sit amet leo metus. Praesent sapien sem, eleifend eget aliquet at, condimentum nec nibh. Fusce vitae odio urna. Pellentesque ac placerat lacus. Mauris vel purus sed risus vestibulum auctor. Ut neque ipsum, lacinia eu turpis nec, dapibus congue mi. Phasellus ut convallis metus.</blockquote>
		
		<p>Another normal paragraph. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque porttitor elit neque, vel suscipit purus vulputate eu. Aliquam commodo quam nec nisl semper, nec facilisis quam volutpat. Sed sit amet leo metus. Praesent sapien sem, eleifend eget aliquet at, condimentum nec nibh. Fusce vitae odio urna. Pellentesque ac placerat lacus. Mauris vel purus sed risus vestibulum auctor. Ut neque ipsum, lacinia eu turpis nec, dapibus congue mi. Phasellus ut convallis metus.</p>
		
		<h1>Header 1</h1>
		<h2>Header 2</h2>
		<h3>Header 3</h3>
		<h4>Header 4</h4>
		<h5>Header 5</h5>
		<h6>Header 6</h6>
    </section>
    <hr />
    <section>
        <h2>Tables</h2>
		<table>
			<tr>
				<th>Column Header 1</th>
				<th>Column Header 2</th>
				<th>Column Header 3</th>
                <th>Column Header 4</th>
				<th>Column Header 5</th>
				<th>Column Header 6</th>
                <th>Column Header 7</th>
			</tr>
			<tr>
				<td>Cell 1</td>
				<td>Cell 2</td>
				<td>Cell 3</td>
                <td>Cell 4</td>
				<td>Cell 5</td>
				<td>Cell 6</td>
                <td>Cell 7</td>
			</tr>
			<tr>
				<td>Cell A</td>
				<td>Cell B</td>
				<td>Cell C</td>
                <td>Cell D</td>
				<td>Cell E</td>
				<td>Cell F</td>
                <td>Cell G</td>
			</tr>
            <tr>
				<td>Cell Alpha</td>
				<td>Cell Beta</td>
				<td>Cell Gamma</td>
                <td>Cell Delta</td>
				<td>Cell Epsilon</td>
				<td>Cell Zeta</td>
                <td>Cell Eta</td>
			</tr>
            <tr>
				<td>Cell 8</td>
				<td>Cell 9</td>
				<td>Cell 10</td>
                <td>Cell 11</td>
				<td>Cell 12</td>
				<td>Cell 13</td>
                <td>Cell 14</td>
			</tr>
			<tr>
				<td>Cell H</td>
				<td>Cell I</td>
				<td>Cell J</td>
                <td>Cell K</td>
				<td>Cell L</td>
				<td>Cell M</td>
                <td>Cell N</td>
			</tr>
            <tr>
				<td>Cell Theta</td>
				<td>Cell Iota</td>
				<td>Cell Kappa</td>
                <td>Cell Lambda</td>
				<td>Cell Mu</td>
				<td>Cell Nu</td>
                <td>Cell Xi</td>
			</tr>
            <tr>
				<td>Cell 15</td>
				<td>Cell 16</td>
				<td>Cell 17</td>
                <td>Cell 18</td>
				<td>Cell 19</td>
				<td>Cell 20</td>
                <td>Cell 21</td>
			</tr>
			<tr>
				<td>Cell O</td>
				<td>Cell P</td>
				<td>Cell Q</td>
                <td>Cell R</td>
				<td>Cell S</td>
				<td>Cell T</td>
                <td>Cell U</td>
			</tr>
            <tr>
				<td>Cell Omicron</td>
				<td>Cell Pi</td>
				<td>Cell Rho</td>
                <td>Cell Sigma</td>
				<td>Cell Tau</td>
				<td>Cell Upsilon</td>
                <td>Cell Phi</td>
			</tr>
		</table>
	</section>
	<hr />
	<section>
		<h2>Lists</h2>
		<p>Lorem ipsum dolor test paragraph test test test.</p>
		<ul>
			<li>This is an unordered list item.</li>
			<li>This is another unordered list item.
				<ul><li>This is a nested unordered list item.
					<ul><li>This is a double-nested unordered list item.
						<ul><li>Another unordered list level.</li></ul></li></ul></li></ul>
			</li>
			<li>This is another unordered list item.
				<ol><li>This is an ordered list item nested within an unordered list.
					<ol><li>This is a nested ordered list item.
						<ol><li>Third level</li></ol>
					</li></ol>
				</li></ol>
			</li>
		</ul>
		
		<ol>
			<li>This is an ordered list item.</li>
			<li>This is another ordered list item.
				<ol><li>This is a nested ordered list item.</li></ol></li>
			<li>This is another ordered list item.
				<ul><li>This is an unordered list item nested within an ordered list.</li></ul></li>
		</ol>
	</section>
    <hr />
    <section>
        <h2>Forms and Buttons</h2>
        <form>
            <label for="test1">Test Label 1</label>
            <input type="text" id="test1" class="form-control" value="test1"/>
        </form>

        <button class="btn btn-default" id="SubmitButton">Submit</button>
        <button class="btn btn-default" id="UploadStatusLabel">Update</button>
        <button class="btn btn-default" id="CancelButton">Cancel</button>
    </section>

</asp:Content>
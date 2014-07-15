<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LEDE_Entity._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<section class="row">
		<p>This is an example of a full-width ROW, using section class "row".</p>
		<p>Here is a paragraph of filler text. Here, have a <a href="#">test list</a>. This is <a href="#" target="_blank">another test link</a> that opens in a new tab. For text, here is <em>emphasized text</em> and <strong>strong text</strong></p>
		
		<blockquote>This is a blockquote. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque porttitor elit neque, vel suscipit purus vulputate eu. Aliquam commodo quam nec nisl semper, nec facilisis quam volutpat. Sed sit amet leo metus. Praesent sapien sem, eleifend eget aliquet at, condimentum nec nibh. Fusce vitae odio urna. Pellentesque ac placerat lacus. Mauris vel purus sed risus vestibulum auctor. Ut neque ipsum, lacinia eu turpis nec, dapibus congue mi. Phasellus ut convallis metus.</blockquote>
		
		<p>Another normal paragraph. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque porttitor elit neque, vel suscipit purus vulputate eu. Aliquam commodo quam nec nisl semper, nec facilisis quam volutpat. Sed sit amet leo metus. Praesent sapien sem, eleifend eget aliquet at, condimentum nec nibh. Fusce vitae odio urna. Pellentesque ac placerat lacus. Mauris vel purus sed risus vestibulum auctor. Ut neque ipsum, lacinia eu turpis nec, dapibus congue mi. Phasellus ut convallis metus.</p>
		
		<h1>Header 1</h1>
		<h2>Header 2</h2>
		<h3>Header 3</h3>
		<h4>Header 4</h4>
		<h5>Header 5</h5>
		<h6>Header 6</h6>

		<table>
			<tr>
				<th>Column Header 1</th>
				<th>Column Header 2</th>
				<th>Column Header 3</th>
			</tr>
			<tr>
				<td>Cell 1</td>
				<td>Cell 2</td>
				<td>Cell 3</td>
			</tr>
			<tr>
				<td>Cell A</td>
				<td>Cell B</td>
				<td>Cell C</td>
			</tr>
		</table>
	</section>
	
	<section class="row">
		<h1>(H1) List 1</h1>
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
</asp:Content>